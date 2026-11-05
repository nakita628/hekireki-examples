class ScimGroupMember < ApplicationRecord
  self.table_name = "scimGroupMember"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :connectionId, presence: true
  validates :membershipKey, presence: true, uniqueness: true
  validates :membershipKey, uniqueness: true

  belongs_to :scim_group, foreign_key: "groupId", inverse_of: :scim_group_members
  belongs_to :scim_user, foreign_key: "scimUserId", inverse_of: :scim_group_members
end
