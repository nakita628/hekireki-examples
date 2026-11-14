class DashboardWidget < ApplicationRecord
  self.table_name = "DashboardWidget"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :gridColumn, default: 0
  attribute :gridRow, default: 0
  attribute :colSpan, default: 1
  attribute :rowSpan, default: 1
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :projectId, presence: true
  validates :name, presence: true
  validates :definition, exclusion: { in: [ nil ] }
end
