class Program < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :durationWeeks, default: 4
  attribute :sessionsPerWeek, default: 3
  attribute :sessionDurationMin, default: 30
  attribute :isPremium, default: true
  attribute :isActive, default: true
  attribute :participantCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :level, { beginner: "BEGINNER", intermediate: "INTERMEDIATE", advanced: "ADVANCED", expert: "EXPERT" },
       validate: true
  enum :type, { bodyweight: "BODYWEIGHT", strength: "STRENGTH", powerlifting: "POWERLIFTING", calisthenic: "CALISTHENIC", plyometrics: "PLYOMETRICS", stretching: "STRETCHING", strongman: "STRONGMAN", cardio: "CARDIO", stabilization: "STABILIZATION", power: "POWER", resistance: "RESISTANCE", crossfit: "CROSSFIT", weightlifting: "WEIGHTLIFTING", biceps: "BICEPS", shoulders: "SHOULDERS", chest: "CHEST", back: "BACK", glutes: "GLUTES", triceps: "TRICEPS", hamstrings: "HAMSTRINGS", quadriceps: "QUADRICEPS", forearms: "FOREARMS", calves: "CALVES", traps: "TRAPS", abdominals: "ABDOMINALS", neck: "NECK", lats: "LATS", adductors: "ADDUCTORS", abductors: "ABDUCTORS", obliques: "OBLIQUES", groin: "GROIN", full_body: "FULL_BODY", rotator_cuff: "ROTATOR_CUFF", hip_flexor: "HIP_FLEXOR", achilles_tendon: "ACHILLES_TENDON", fingers: "FINGERS", dumbbell: "DUMBBELL", kettlebells: "KETTLEBELLS", barbell: "BARBELL", smith_machine: "SMITH_MACHINE", body_only: "BODY_ONLY", other: "OTHER", bands: "BANDS", ez_bar: "EZ_BAR", machine: "MACHINE", desk: "DESK", pullup_bar: "PULLUP_BAR", none: "NONE", cable: "CABLE", medicine_ball: "MEDICINE_BALL", swiss_ball: "SWISS_BALL", foam_roll: "FOAM_ROLL", weight_plate: "WEIGHT_PLATE", trx: "TRX", box: "BOX", ropes: "ROPES", spin_bike: "SPIN_BIKE", step: "STEP", bosu: "BOSU", tyre: "TYRE", sandbag: "SANDBAG", pole: "POLE", bench: "BENCH", wall: "WALL", bar: "BAR", rack: "RACK", car: "CAR", sled: "SLED", chain: "CHAIN", skierg: "SKIERG", rope: "ROPE", na: "NA", isolation: "ISOLATION", compound: "COMPOUND" },
       prefix: true, validate: true
  enum :visibility, { draft: "DRAFT", published: "PUBLISHED", archived: "ARCHIVED" }, default: :draft, validate: true

  validates :slug, presence: true, uniqueness: true
  validates :slugEn, presence: true, uniqueness: true
  validates :slugEs, presence: true, uniqueness: true
  validates :slugPt, presence: true, uniqueness: true
  validates :slugRu, presence: true, uniqueness: true
  validates :slugZhCn, presence: true, uniqueness: true
  validates :title, presence: true
  validates :titleEn, presence: true
  validates :titleEs, presence: true
  validates :titlePt, presence: true
  validates :titleRu, presence: true
  validates :titleZhCn, presence: true
  validates :description, presence: true
  validates :descriptionEn, presence: true
  validates :descriptionEs, presence: true
  validates :descriptionPt, presence: true
  validates :descriptionRu, presence: true
  validates :descriptionZhCn, presence: true
  validates :category, presence: true
  validates :image, presence: true

  has_many :weeks, class_name: "ProgramWeek", foreign_key: "programId", inverse_of: :program, dependent: :destroy
  has_many :enrollments, class_name: "UserProgramEnrollment", foreign_key: "programId", inverse_of: :program,
                         dependent: :restrict_with_error
  has_many :coaches, class_name: "ProgramCoach", foreign_key: "programId", inverse_of: :program, dependent: :destroy
end
