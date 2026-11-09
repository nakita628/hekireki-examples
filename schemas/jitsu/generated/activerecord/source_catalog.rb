class SourceCatalog < ApplicationRecord
  self.table_name = "source_catalog"
  self.primary_key = %w[package version key]

  validates :status, presence: true
end
