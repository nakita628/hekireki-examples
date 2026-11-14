class CustomGraph < ApplicationRecord
  self.table_name = "CustomGraph"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :kind, default: "builder"
  attribute :gridColumn, default: 0
  attribute :gridRow, default: 0
  attribute :colSpan, default: 1
  attribute :rowSpan, default: 1
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :graph, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :custom_graphs
  belongs_to :dashboard, foreign_key: "dashboardId", optional: true, inverse_of: :graphs
  has_one :trigger, foreign_key: "customGraphId", inverse_of: :custom_graph, dependent: :destroy
  has_many :trigger_sent, foreign_key: "customGraphId", inverse_of: :custom_graph, dependent: :destroy
end
