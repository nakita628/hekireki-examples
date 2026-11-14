class RoleBinding < ApplicationRecord
  self.table_name = "RoleBinding"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { admin: "ADMIN", member: "MEMBER", viewer: "VIEWER", custom: "CUSTOM" }, validate: true
  enum :scopeType, { organization: "ORGANIZATION", team: "TEAM", project: "PROJECT" }, validate: true

  validates :scopeId, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :role_bindings
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :role_bindings
  belongs_to :group, foreign_key: "groupId", optional: true, inverse_of: :role_bindings
  belongs_to :api_key, foreign_key: "apiKeyId", optional: true, inverse_of: :role_bindings
  belongs_to :custom_role, foreign_key: "customRoleId", optional: true, inverse_of: :role_bindings
end
