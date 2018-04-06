using UnityEngine;
using UnityEngine.UI;

namespace script {
    public class PersonController : MonoBehaviour {

        public Person person;

        public Animator animator;
        public Animator applyAbility;

        public GameObject heroLevel;

        public void Start() {
            animator = transform.Find("model").GetComponent<Animator>();
            applyAbility = transform.Find("model/animation/abilityAnimation").GetComponent<Animator>();

            heroLevel = transform.Find("HeroLevel").gameObject;
            heroLevel.transform.GetComponent<Text>().text = person.level.ToString();

            transform.Find("model/person/body/leftHand/topHand/middleHand/bottomHand/weapon")
                    .GetComponent<SpriteRenderer>().sprite = person.activeInventory.leftHand != null ? person.activeInventory.leftHand.image : null;
            transform.Find("model/person/body/rightHand/topHand/middleHand/bottomHand/shield")
                .GetComponent<SpriteRenderer>().sprite = person.activeInventory.rightHand != null ? person.activeInventory.rightHand.image : null;
        }

        public void Update() {
            if (!(person.health <= 0)) return;
            person.isAlive = false;
            setDead();
        }

        public void applyEffect() {
            if (EventQueueSingleton.queue.fastFight) return;
            Debug.Log(Time.fixedTime + ":" + person.name + " start casting spell");
            applyAbility.SetTrigger(AnimatorConstants.MODEL_APPLY_ANIMATION);
        }

        public void castAbility() {
            if (EventQueueSingleton.queue.fastFight) return;
            Debug.Log(Time.fixedTime + ":" + person.name + " start casting spell");
            animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISCAST);
        }

        public void meleeAttackAbility() {
            if (EventQueueSingleton.queue.fastFight) return;
            Debug.Log(Time.fixedTime + ":" + person.name + " start casting attack");
            animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISATTACK);
        }

        public void hittenTrigger() {
            if (EventQueueSingleton.queue.fastFight) return;
            Debug.Log(Time.fixedTime + ":" + person.name + " been hitten");
            animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISHITTEN);
        }

        public void blockTrigger() {
            if (EventQueueSingleton.queue.fastFight) return;
            Debug.Log(Time.fixedTime + ":" + person.name + " been hitten, blocked attack");
            animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISBLOCK);
        }

        public void magicShieldTrigger() {
            if (EventQueueSingleton.queue.fastFight) return;
            Debug.Log(Time.fixedTime + ":" + person.name + " been hitten, magic shield");
            animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISMAGIC_SHIELD);
        }

        public void setDead() {
            if (EventQueueSingleton.queue.fastFight) return;
            Debug.Log(Time.fixedTime + ":" + person.name + " is dead");
            animator.SetBool(AnimatorConstants.MODEL_ANIMATOR_ISDEAD, true);
        }

    }
}
