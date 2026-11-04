class DesktopMobileDevices < ApplicationRecord
  attribute :approved, default: false
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :deviceOs, presence: true
  validates :deviceName, presence: true
  validates :token, presence: true, uniqueness: true

  belongs_to :user, class_name: "Users", foreign_key: "userId", optional: true, inverse_of: :desktop_mobile_devices
end
