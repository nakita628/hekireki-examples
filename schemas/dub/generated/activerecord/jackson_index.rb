class JacksonIndex < ApplicationRecord
  self.table_name = "jackson_index"

  validates :key, presence: true, length: { maximum: 250 }
  validates :storeKey, presence: true, length: { maximum: 250 }
end
