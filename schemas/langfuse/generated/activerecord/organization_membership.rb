class OrganizationMembership < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :role, { owner: "OWNER", admin: "ADMIN", member: "MEMBER", viewer: "VIEWER", none: "NONE", ai_gateway: "AI_GATEWAY" },
       prefix: true, validate: true

  validates :org_id, uniqueness: { scope: :user_id }

  belongs_to :organization, foreign_key: "org_id", inverse_of: :organization_memberships
  belongs_to :user, inverse_of: :organization_memberships
  has_many :project_memberships, foreign_key: "org_membership_id", inverse_of: :organization_membership,
                                 dependent: :destroy
end
