class WorkspaceOptions < ApplicationRecord
  self.table_name = "WorkspaceOptions"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :namespace, presence: true
  validates :value, exclusion: { in: [ nil ] }

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :workspace_options
end
