using UnityEngine;

namespace Onion2D.Movement
{
	public class GroundCheck : MonoBehaviour
	{
		[SerializeField] Collider2D groundCheckUnderThisCollider;
		[SerializeField] LayerMask groundLayer;
		[SerializeField] bool drawGizmo;
		[SerializeField] float groundBoxSizeY = .15f;
		[SerializeField] float groundBoxSizeX = .95f;
		new Rigidbody2D rigidbody2D;

		private void Awake()
		{
			rigidbody2D = GetComponent<Rigidbody2D>();
		}

		public Vector2 Center
		{
			get
			{
				float gravityScale = (rigidbody2D != null) ? rigidbody2D.gravityScale : 1f;
				Vector2 center = groundCheckUnderThisCollider.bounds.center - Vector3.up * groundCheckUnderThisCollider.bounds.extents.y * Mathf.Sign(gravityScale);
				center.y -= groundBoxSizeY * .5f;
				return center;
			}
		}
		public bool IsGrounded => CheckForGround();

		public bool CheckForGround()
		{
			var col = Physics2D.OverlapBox(Center, new Vector2(groundCheckUnderThisCollider.bounds.size.x * groundBoxSizeX, groundBoxSizeY), 0, groundLayer);
			return col is null ? false : true;
		}

		public bool IsGroundedWith(LayerMask layersConsideredGround)
		{
			var col = Physics2D.OverlapBox(Center, new Vector2(groundCheckUnderThisCollider.bounds.size.x * groundBoxSizeX, groundBoxSizeY), 0, layersConsideredGround);
			return col is null ? false : true;
		}


		void OnDrawGizmos()
		{
			if (drawGizmo)
			{
				Gizmos.color = Color.red;
				Vector2 size = new Vector2(groundCheckUnderThisCollider.bounds.size.x * groundBoxSizeX, groundBoxSizeY);
				Gizmos.DrawWireCube(Center, size);
			}
		}
	}
}
