class DataContent < ApplicationRecord
  self.table_name = "DataContent"
  self.inheritance_column = nil

  enum :type, { s3_path: "S3_PATH", bytes: "BYTES", bytes_64: "BYTES_64" }, validate: true

  validates :data, presence: true
  validates :metadata, exclusion: { in: [ nil ] }

  has_many :envelope_contents, foreign_key: "dataContentId", inverse_of: :data_content, dependent: :nullify
end
