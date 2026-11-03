class Folder < ApplicationRecord
  self.table_name = "Folder"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :pinned, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :visibility, { everyone: "EVERYONE", manager_and_above: "MANAGER_AND_ABOVE", admin: "ADMIN" }, default: :everyone,
                                                                                                      validate: true
  enum :type, { document: "DOCUMENT", template: "TEMPLATE" }, validate: true

  validates :name, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :folders
  belongs_to :team, foreign_key: "teamId", inverse_of: :folders
  belongs_to :parent, class_name: "Folder", foreign_key: "parentId", optional: true, inverse_of: :subfolders
  has_many :envelopes, foreign_key: "folderId", inverse_of: :folder, dependent: :nullify
  has_many :subfolders, class_name: "Folder", foreign_key: "parentId", inverse_of: :parent, dependent: :destroy
end
