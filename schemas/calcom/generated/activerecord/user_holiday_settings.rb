class UserHolidaySettings < ApplicationRecord
  self.table_name = "UserHolidaySettings"

  attribute :disabledIds, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :userId, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :holiday_settings
end
