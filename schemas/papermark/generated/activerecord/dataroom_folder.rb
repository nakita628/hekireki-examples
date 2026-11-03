class DataroomFolder < ApplicationRecord
  self.table_name = "DataroomFolder"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :path, presence: true, uniqueness: { scope: :dataroomId }

  belongs_to :parent_folder, class_name: "DataroomFolder", foreign_key: "parentId", optional: true,
                             inverse_of: :child_folders
  belongs_to :dataroom, foreign_key: "dataroomId", inverse_of: :folders
  has_many :documents, class_name: "DataroomDocument", foreign_key: "folderId", inverse_of: :folder, dependent: :nullify
  has_many :child_folders, class_name: "DataroomFolder", foreign_key: "parentId", inverse_of: :parent_folder,
                           dependent: :nullify
  has_many :upload_tasks, class_name: "Task", foreign_key: "uploadFolderId", inverse_of: :upload_folder,
                          dependent: :nullify
end
