class MembershipInvitation < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :org_role, { owner: "OWNER", admin: "ADMIN", member: "MEMBER", viewer: "VIEWER", none: "NONE", ai_gateway: "AI_GATEWAY" },
       prefix: true, validate: true
  enum :project_role, { owner: "OWNER", admin: "ADMIN", member: "MEMBER", viewer: "VIEWER", none: "NONE", ai_gateway: "AI_GATEWAY" },
       prefix: true, validate: { allow_nil: true }

  validates :email, presence: true, uniqueness: { scope: :org_id }

  belongs_to :organization, foreign_key: "org_id", inverse_of: :membership_invitation
  belongs_to :project, optional: true, inverse_of: :invitations
  belongs_to :invited_by_user, class_name: "User", optional: true, inverse_of: :invitations
end
