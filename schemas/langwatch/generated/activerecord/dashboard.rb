class Dashboard < ApplicationRecord
  self.table_name = "Dashboard"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :order, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :scope, { private: "PRIVATE", project: "PROJECT", organization: "ORGANIZATION" }, default: :project, prefix: true,
                                                                                         validate: true

  validates :name, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :dashboards
  has_many :graphs, class_name: "CustomGraph", foreign_key: "dashboardId", inverse_of: :dashboard, dependent: :destroy
end
