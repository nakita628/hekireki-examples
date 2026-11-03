class FolderUser < ApplicationRecord
  self.table_name = "FolderUser"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { owner: "owner", editor: "editor", viewer: "viewer" }, validate: { allow_nil: true }

  validates :folderId, uniqueness: { scope: :userId }

  belongs_to :folder, foreign_key: "folderId", inverse_of: :users
  belongs_to :user, foreign_key: "userId", inverse_of: :folders
end
