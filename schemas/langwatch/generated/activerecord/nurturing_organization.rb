class NurturingOrganization < ApplicationRecord
  self.table_name = "NurturingOrganization"
  self.primary_key = "organizationId"

  attribute :seeded, default: false
  attribute :evaluationCount, default: 0
  attribute :simulationRunCount, default: 0
  attribute :codingAgentTypes, default: -> { [] }
  attribute :codingActiveDays, default: 0
  attribute :codingTerminalTypes, default: -> { [] }
  attribute :codingRepositoryKeys, default: -> { [] }
  attribute :codingFirstDays, default: -> { [] }
  alias_attribute :updated_at, :updatedAt
end
