class AvailabilitySchedule < ApplicationRecord
  self.table_name = "AvailabilitySchedule"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isDefault, default: false
  attribute :timezone, default: "UTC"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :id, uniqueness: { scope: :emailAccountId }
  validates :name, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :availability_schedules
  has_many :windows, class_name: "AvailabilityWindow", foreign_key: "availabilityScheduleId",
                     inverse_of: :availability_schedule, dependent: :destroy
  has_many :booking_links, foreign_key: [ :availabilityScheduleId, :emailAccountId ],
                           primary_key: [ :id, :emailAccountId ], inverse_of: :availability_schedule,
                           dependent: :restrict_with_error
end
