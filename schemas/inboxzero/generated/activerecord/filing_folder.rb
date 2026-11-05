class FilingFolder < ApplicationRecord
  self.table_name = "FilingFolder"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :folderId, presence: true, uniqueness: { scope: :emailAccountId }
  validates :folderName, presence: true
  validates :folderPath, presence: true

  belongs_to :drive_connection, foreign_key: "driveConnectionId", inverse_of: :filing_folders
  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :filing_folders
end
