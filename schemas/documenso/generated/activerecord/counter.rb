class Counter < ApplicationRecord
  self.table_name = "Counter"

  validates :value, presence: true
end
