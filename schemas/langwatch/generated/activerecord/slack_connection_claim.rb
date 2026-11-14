class SlackConnectionClaim < ApplicationRecord
  self.table_name = "slack_connection_claim"
  self.primary_key = %w[connectionId claimantId]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :claimantLabel, presence: true
  validates :organizationId, presence: true
  validates :projectId, presence: true
end
