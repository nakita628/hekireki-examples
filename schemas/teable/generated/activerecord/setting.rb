class Setting < ApplicationRecord
  self.table_name = "setting"

  alias_attribute :updated_at, :last_modified_time

  validates :name, presence: true, uniqueness: true
  validates :created_by, presence: true
end
