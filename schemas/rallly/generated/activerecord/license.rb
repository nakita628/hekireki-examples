class License < ApplicationRecord
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }
  attribute :white_label_addon, default: false

  enum :type, { plus: "PLUS", organization: "ORGANIZATION", enterprise: "ENTERPRISE" }, validate: true
  enum :status, { active: "ACTIVE", revoked: "REVOKED" }, default: :active, validate: true

  validates :license_key, presence: true, uniqueness: true
  validates :idempotency_key, uniqueness: { allow_nil: true }

  has_many :validations, class_name: "LicenseValidation", inverse_of: :license, dependent: :destroy
end
