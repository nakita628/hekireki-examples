class ActivationCode < ApplicationRecord
  self.table_name = "ActivationCode"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :maxMembersLite, default: 0
  attribute :liteSeatsUnlimited, default: false
  attribute :services, default: -> { [] }
  attribute :reusable, default: false
  attribute :redemptionCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :codeHash, presence: true, uniqueness: true
  validates :codeHint, presence: true
  validates :organizationId, presence: true
  validates :organizationName, presence: true
  validates :email, presence: true
  validates :planType, presence: true
  validates :maxMembers, presence: true
  validates :licenseTermDays, presence: true
  validates :expiresAt, presence: true
end
