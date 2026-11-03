class FolderAccessRequest < ApplicationRecord
  self.table_name = "FolderAccessRequest"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :folderId, uniqueness: { scope: :userId }

  belongs_to :folder, foreign_key: "folderId", inverse_of: :access_requests
  belongs_to :user, foreign_key: "userId", inverse_of: :folder_access_requests
end
