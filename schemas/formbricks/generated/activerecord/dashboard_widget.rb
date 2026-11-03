class DashboardWidget < ApplicationRecord
  self.table_name = "DashboardWidget"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :layout, default: -> { JSON.parse("{\"x\":0,\"y\":0,\"w\":4,\"h\":3}") }
  attribute :order, default: 0

  validates :dashboardId, uniqueness: { scope: :chartId }

  belongs_to :dashboard, foreign_key: "dashboardId", inverse_of: :widgets
  belongs_to :chart, foreign_key: "chartId", inverse_of: :widgets
end
