class OrgMember < ApplicationRecord
  self.table_name = "OrgMember"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { admin: "ADMIN", member: "MEMBER" }, default: :member, validate: true

  validates :organizationId, uniqueness: { scope: :userId }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :members
  belongs_to :user, foreign_key: "userId", inverse_of: :org_memberships
  has_many :environments, class_name: "RuntimeEnvironment", foreign_key: "orgMemberId", inverse_of: :org_member,
                          dependent: :nullify
end
