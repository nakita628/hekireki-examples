class Roles < ApplicationRecord
  validates :name, presence: true, uniqueness: true

  has_and_belongs_to_many :users, class_name: "Users", join_table: "_rolesTousers", foreign_key: "A",
                                  association_foreign_key: "B"
end
