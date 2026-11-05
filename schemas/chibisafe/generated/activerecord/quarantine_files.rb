class QuarantineFiles < ApplicationRecord
  self.table_name = "quarantineFiles"

  validates :fileId, uniqueness: true
  validates :name, presence: true

  belongs_to :file, class_name: "Files", foreign_key: "fileId", primary_key: "uuid", inverse_of: :quarantine_file
end
