class OrganizationUser < ApplicationRecord
  self.table_name = "OrganizationUser"
  self.primary_key = %w[userId organizationId]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { admin: "ADMIN", member: "MEMBER", external: "EXTERNAL", developer: "DEVELOPER" }, validate: true

  belongs_to :user, foreign_key: "userId", inverse_of: :org_memberships
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :members
end
