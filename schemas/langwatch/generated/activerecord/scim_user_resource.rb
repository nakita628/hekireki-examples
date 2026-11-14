class ScimUserResource < ApplicationRecord
  self.table_name = "ScimUserResource"
  self.primary_key = %w[organizationId userId]

  attribute :active, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :userName, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :scim_user_resources
end
