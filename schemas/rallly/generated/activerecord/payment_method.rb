class PaymentMethod < ApplicationRecord
  self.inheritance_column = nil

  validates :type, presence: true
  validates :data, exclusion: { in: [ nil ] }

  belongs_to :user, inverse_of: :payment_methods
end
