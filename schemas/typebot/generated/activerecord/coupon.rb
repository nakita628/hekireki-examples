class Coupon < ApplicationRecord
  self.table_name = "Coupon"
  self.primary_key = "code"

  validates :userPropertiesToUpdate, exclusion: { in: [ nil ] }
end
