class Folder < ApplicationRecord
  self.table_name = "Folder"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :hiddenInAllDocuments, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :path, presence: true, uniqueness: { scope: :teamId }

  belongs_to :parent_folder, class_name: "Folder", foreign_key: "parentId", optional: true, inverse_of: :child_folders
  belongs_to :team, foreign_key: "teamId", inverse_of: :folders
  has_many :documents, foreign_key: "folderId", inverse_of: :folder, dependent: :nullify
  has_many :child_folders, class_name: "Folder", foreign_key: "parentId", inverse_of: :parent_folder,
                           dependent: :nullify
end
