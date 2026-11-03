class JacksonIndex < ApplicationRecord
  self.table_name = "jackson_index"

  validates :key, presence: true
  validates :storeKey, presence: true
end
