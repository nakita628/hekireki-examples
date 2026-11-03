class Property < ApplicationRecord
  self.table_name = "Property"
  self.primary_key = "key"

  validates :value, presence: true
end
