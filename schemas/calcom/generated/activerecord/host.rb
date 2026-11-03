class Host < ApplicationRecord
  self.table_name = "Host"
  self.primary_key = %w[userId eventTypeId]

  attribute :isFixed, default: false
  alias_attribute :created_at, :createdAt

  belongs_to :user, foreign_key: "userId", inverse_of: :hosts
  belongs_to :event_type, foreign_key: "eventTypeId", inverse_of: :hosts
  belongs_to :schedule, foreign_key: "scheduleId", optional: true, inverse_of: :host
  belongs_to :group, class_name: "HostGroup", foreign_key: "groupId", optional: true, inverse_of: :hosts
  belongs_to :member, class_name: "Membership", foreign_key: "memberId", optional: true, inverse_of: :host
  has_one :location, class_name: "HostLocation", foreign_key: [ :userId, :eventTypeId ], inverse_of: :host,
                     dependent: :destroy
end
