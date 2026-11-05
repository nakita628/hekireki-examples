class DriveConnection < ApplicationRecord
  self.table_name = "DriveConnection"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isConnected, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :provider, presence: true, uniqueness: { scope: :emailAccountId }
  validates :email, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :drive_connections
  has_many :document_filings, foreign_key: "driveConnectionId", inverse_of: :drive_connection, dependent: :destroy
  has_many :filing_folders, foreign_key: "driveConnectionId", inverse_of: :drive_connection, dependent: :destroy
  has_many :attachment_sources, foreign_key: "driveConnectionId", inverse_of: :drive_connection, dependent: :destroy
end
