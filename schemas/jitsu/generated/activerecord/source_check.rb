class SourceCheck < ApplicationRecord
  self.table_name = "source_check"
  self.primary_key = "key"

  validates :package, presence: true
  validates :version, presence: true
  validates :status, presence: true
end
