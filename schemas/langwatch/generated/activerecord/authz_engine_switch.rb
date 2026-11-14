class AuthzEngineSwitch < ApplicationRecord
  self.table_name = "AuthzEngineSwitch"
  self.primary_key = "organizationId"

  attribute :pinned, default: false
  alias_attribute :updated_at, :updatedAt

  validates :onEngine, inclusion: { in: [ true, false ] }
  validates :switchedAt, presence: true
end
