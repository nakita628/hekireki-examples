class ExerciseAttributeValue < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :value,
            inclusion: { in: %w[BODYWEIGHT STRENGTH POWERLIFTING CALISTHENIC PLYOMETRICS STRETCHING STRONGMAN CARDIO STABILIZATION POWER RESISTANCE CROSSFIT WEIGHTLIFTING BICEPS SHOULDERS CHEST BACK GLUTES TRICEPS HAMSTRINGS QUADRICEPS FOREARMS CALVES TRAPS ABDOMINALS NECK LATS ADDUCTORS ABDUCTORS OBLIQUES GROIN FULL_BODY ROTATOR_CUFF HIP_FLEXOR ACHILLES_TENDON FINGERS DUMBBELL KETTLEBELLS BARBELL SMITH_MACHINE BODY_ONLY OTHER BANDS EZ_BAR MACHINE DESK PULLUP_BAR NONE CABLE MEDICINE_BALL SWISS_BALL FOAM_ROLL WEIGHT_PLATE TRX BOX ROPES SPIN_BIKE STEP BOSU TYRE SANDBAG POLE BENCH WALL BAR RACK CAR SLED CHAIN SKIERG ROPE NA ISOLATION COMPOUND] }

  validates :attributeNameId, uniqueness: { scope: :value }

  belongs_to :attribute_name, class_name: "ExerciseAttributeName", foreign_key: "attributeNameId", inverse_of: :values
  has_many :exercise_attributes, foreign_key: "attributeValueId", inverse_of: :attribute_value,
                                 dependent: :restrict_with_error
end
