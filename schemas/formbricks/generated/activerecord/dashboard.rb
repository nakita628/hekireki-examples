class Dashboard < ApplicationRecord
  self.table_name = "Dashboard"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :filters, default: -> { JSON.parse("[]") }

  validates :name, presence: true, uniqueness: { scope: :workspaceId }

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :dashboards
  belongs_to :creator, class_name: "User", foreign_key: "createdBy", optional: true, inverse_of: :dashboards
  has_many :widgets, class_name: "DashboardWidget", foreign_key: "dashboardId", inverse_of: :dashboard,
                     dependent: :destroy
end
