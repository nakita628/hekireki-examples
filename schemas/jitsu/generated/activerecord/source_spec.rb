class SourceSpec < ApplicationRecord
  self.table_name = "source_spec"
  self.primary_key = %w[package version]
end
