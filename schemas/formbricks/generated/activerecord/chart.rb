class Chart < ApplicationRecord
  self.table_name = "Chart"
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :query, default: -> { JSON.parse("{}") }
  attribute :config, default: -> { JSON.parse("{}") }

  enum :type, { area: "area", bar: "bar", pie: "pie", big_number: "big_number", matrix: "matrix" }, validate: true

  validates :name, presence: true, uniqueness: { scope: :workspaceId }

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :charts
  belongs_to :creator, class_name: "User", foreign_key: "createdBy", optional: true, inverse_of: :charts
  belongs_to :feedback_directory, foreign_key: "feedbackDirectoryId", inverse_of: :charts
  has_many :widgets, class_name: "DashboardWidget", foreign_key: "chartId", inverse_of: :chart, dependent: :destroy
end
