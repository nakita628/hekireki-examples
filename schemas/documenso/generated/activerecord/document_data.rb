class DocumentData < ApplicationRecord
  self.table_name = "DocumentData"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }

  enum :type, { s3_path: "S3_PATH", bytes: "BYTES", bytes_64: "BYTES_64" }, validate: true

  validates :data, presence: true
  validates :initialData, presence: true

  has_one :envelope_item, foreign_key: "documentDataId", inverse_of: :document_data, dependent: :destroy
end
