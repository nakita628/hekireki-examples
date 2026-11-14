class DashboardFavourite < ApplicationRecord
  self.table_name = "DashboardFavourite"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  validates :userId, presence: true, uniqueness: { scope: :dashboardId, conditions: -> { where.not(dashboardId: nil) } }
  validates :projectId, presence: true,
                        uniqueness: { scope: [ :userId, :templateId ], conditions: -> { where.not(templateId: nil) } }
  validates :position, presence: true
end
