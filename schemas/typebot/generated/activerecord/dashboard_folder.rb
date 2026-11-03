class DashboardFolder < ApplicationRecord
  self.table_name = "DashboardFolder"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true

  belongs_to :parent_folder, class_name: "DashboardFolder", foreign_key: "parentFolderId", optional: true,
                             inverse_of: :children_folder
  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :folders
  has_many :children_folder, class_name: "DashboardFolder", foreign_key: "parentFolderId", inverse_of: :parent_folder,
                             dependent: :nullify
  has_many :typebots, foreign_key: "folderId", inverse_of: :folder, dependent: :nullify
end
