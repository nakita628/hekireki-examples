class Grant < ApplicationRecord
  self.table_name = "Grant"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :principalType, { user: "USER", api_key: "API_KEY", group: "GROUP", team: "TEAM", organization: "ORGANIZATION", project: "PROJECT", anyone: "ANYONE" },
       prefix: true, validate: true
  enum :scopeType, { organization: "ORGANIZATION", team: "TEAM", project: "PROJECT", resource: "RESOURCE", platform: "PLATFORM" },
       validate: true

  validates :organizationId, presence: true
  validates :source, presence: true
  validates :scopeId, presence: true
  validates :token, uniqueness: { allow_nil: true }
  validates :occurredAt, presence: true
end
