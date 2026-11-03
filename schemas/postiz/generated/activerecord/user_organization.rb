class UserOrganization < ApplicationRecord
  self.table_name = "UserOrganization"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :disabled, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { superadmin: "SUPERADMIN", admin: "ADMIN", user: "USER" }, default: :user, validate: true

  validates :userId, uniqueness: { scope: :organizationId }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :users
  belongs_to :user, foreign_key: "userId", inverse_of: :organizations
end
