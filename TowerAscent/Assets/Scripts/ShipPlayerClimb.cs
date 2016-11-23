namespace VRTK
{
    using UnityEngine;

    public class ShipPlayerClimb : VRTK_PlayerClimb
    {
        private Vector3 MyStartPosition = Vector3.zero;
        private Vector3 AnchorStartPosition = Vector3.zero;
        private Quaternion AnchorStartRotation = Quaternion.identity;

        void Start()
        {
            base.PlayerClimbStarted += new PlayerClimbEventHandler(MyPlayerClimbStarted);
        }

        new public void Update()
        {
            if (isClimbing)
            {
                Vector3 AnchorNewPosition = climbingObject.transform.position;
                Vector3 AnchorDelta = AnchorNewPosition - AnchorStartPosition;
                startPosition = MyStartPosition + AnchorDelta;

                base.Update();

                Quaternion AnchorNewRotation = climbingObject.transform.rotation;
                Quaternion RotationDelta = AnchorNewRotation * Quaternion.Inverse(AnchorStartRotation);

                Vector3 DeltaNewAnchorToMyNewPosition = transform.position - AnchorNewPosition;
                Vector3 RotatedVector = RotationDelta * DeltaNewAnchorToMyNewPosition;

                Vector3 Target = AnchorNewPosition + RotatedVector;

                transform.position = Vector3.Slerp(transform.position, Target, Time.deltaTime * 100f);
            }

        }

        void MyPlayerClimbStarted(object sender, PlayerClimbEventArgs e)
        {
            MyStartPosition = transform.position;
            AnchorStartPosition = e.target.transform.position;
            AnchorStartRotation = e.target.transform.rotation;
        }
    }
}
