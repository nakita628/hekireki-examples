class FunctionsServer < ApplicationRecord
  self.table_name = "FunctionsServer"
  self.primary_key = %w[workspaceId class]

  attribute :connections, default: -> { [] }
  attribute :emptyConnections, default: -> { [] }
  attribute :profileBuilders, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :deploymentId, presence: true
end
