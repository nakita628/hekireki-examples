class LicenseValidation < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }

  belongs_to :license, inverse_of: :validations
end
