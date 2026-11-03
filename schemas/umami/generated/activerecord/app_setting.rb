class AppSetting < ApplicationRecord
  self.table_name = "app_setting"
  self.primary_key = "key"

  validates :value, presence: true
end
