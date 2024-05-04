using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Fly
{
    public class Skill : MonoBehaviour
    {
        public float lifeTime = 3f;

        public int skillID;

        public int damage;

        Vector3 _moveDirection;
        public float speed = 0;

        private PlayerHeart _playerHeart;
        private Playermovee _playerMove;

        private SpriteRenderer _spriteRenderer;
        private GameObject _player;

        private bool _isPlayerHealth = false;

        private Animator _animator;

        private bool _isUsed = false;

        private void Awake()
        {
            _playerHeart = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHeart>();
            _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<Playermovee>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Start is called before the first frame update
        void Start()
        {
            _player = GameManager.instance.player;
            _animator = GetComponent<Animator>();
            _moveDirection = new Vector3(speed * Time.deltaTime, 0, 0);

            if (skillID == 2)
            {
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Debug.Log(isplayerHealth);
            //move();
            _spriteRenderer.sortingOrder = 1 - (int)_player.transform.position.y;

            if (_isPlayerHealth)
            {
                // 增加玩家的生命值，根据恢复速率和 Time.deltaTime 来计算
                lifeTime -= Time.deltaTime;
                //Debug.Log($"lifeTime = {lifeTime} , 玩家回血{damage * Time.deltaTime}");
                damage = Random.Range(1, 6);
                _playerHeart.healHp(damage * Time.deltaTime);
            }

            Move();
        }

        private void UseSkill(GameObject monster = null)
        {
            switch (skillID)
            {
                case 0: //普通子彈
                    StartCoroutine(HurtMonster(monster));
                    Destroy(gameObject);
                    break;
                case 1: //熾熱
                    //monsterAI.isHurt(damage);
                    break;
                case 2: //生命
                    Debug.Log("有觸發到回復氧氣喔~");
                    lifeTime += 10;
                    damage = Random.Range(20, 31);
                    StartCoroutine(Heal(damage));
                    StartCoroutine(HurtMonster(monster));
                    _animator.Play("animaBom");
                    break;
                case 3: //氧氣
                    _playerHeart.healO2(damage);
                    _playerMove.startplayerSpeedUP(5f);
                    lifeTime = 5f;
                    break;
                case 4: //閃電
                    break;
                case 5: //光明
                    break;
                case 6: //傳送
                    break;
                case 7: //補血治癒圈 生命+(還有少東西)
                    _playerHeart.healO2(damage);
                    _isPlayerHealth = true;
                    break;
            }
            //Destroy(gameObject);
        }

        void Move()
        {
            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0)
            {
                _moveDirection = new Vector2(0, 0); // 停止移動
                Destroy(gameObject);
            }
            else if (lifeTime > 0.1f)
            {
                transform.Translate(_moveDirection);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("boss"))
            {
                UseSkill(other.gameObject);
            }

            if (other.CompareTag("enemy"))
            {
                UseSkill(other.gameObject);

                _moveDirection = new Vector2(0, 0); // 停止移動
            }

            if (other.CompareTag("Object") && other.isTrigger == false)
            {
                if (skillID != 3) //不是氧氣蝴蝶
                {
                    _moveDirection = new Vector2(0, 0); // 停止移動
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (skillID == 3 && !_isUsed) //氧氣
                {
                    var random = Random.Range(0, 3);
                    if (random == 0)
                    {
                        Debug.Log("要使用氧氣技能");
                        _isUsed = true;
                        UseSkill();
                    }
                }

                //isplayerHealth = true;
                if (skillID == 7) //生命(雙)
                {
                    // 增加玩家的生命值，根据恢复速率和 Time.deltaTime 来计算
                    lifeTime -= Time.deltaTime;
                    //Debug.Log($"lifeTime = {lifeTime} , 玩家回血{damage * Time.deltaTime}");
                    _playerHeart.healHp(damage * Time.deltaTime);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (skillID == 7) //生命(雙)
                {
                    UseSkill();
                    _isPlayerHealth = false;
                }
            }
        }


        IEnumerator Heal(int healHp)
        {
            //UIControl.instance.DebugText("-回覆血量效果動畫");
            for (int i = 0; i < healHp; i++)
            {
                //Debug.Log($"回復了1d血 還有{5-i}次");
                var healHpNum = Random.Range(1, 4);
                _playerHeart.healHp(healHpNum);
                yield return new WaitForSeconds(0.5f);
            }

            Destroy(gameObject);
        }

        IEnumerator HurtMonster(GameObject monster)
        {
            //UIControl.instance.DebugText("-回覆血量效果動畫");
            var randomdamage = Random.Range(1, 5);

            if (monster.CompareTag("boss"))
            {
                if (skillID == 0)
                {
                    var randomLuck = Random.Range(0f, 1f);

                    if (randomLuck <= 0.94f)
                    {
                    }
                    else if (randomLuck <= 0.99)
                    {
                        randomdamage = Random.Range(50, 101);
                    }
                    else if (randomLuck <= 1)
                    {
                        randomdamage = 999;
                    }
                }

                for (int i = 0; i < damage; i++)
                {
                    if (monster != null)
                    {
                        monster.GetComponent<BossAI>().damage(randomdamage, transform.position);
                        yield return new WaitForSeconds(0.5f);
                    }
                }

                Destroy(gameObject);
            }
            else if (monster.CompareTag("enemy"))
            {
                for (int i = 0; i < damage; i++)
                {
                    if (monster != null)
                    {
                        monster.GetComponent<monsterAI>().isHurt(randomdamage);
                        yield return new WaitForSeconds(0.5f);
                    }
                }

                Destroy(gameObject);
            }
        }

        public void closeImage()
        {
            GetComponent<SpriteRenderer>().sprite = null;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}