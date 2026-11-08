package model

import (
	"database/sql/driver"
	"fmt"
	"strconv"
	"time"

	"github.com/lucsky/cuid"
	"gorm.io/datatypes"
	"gorm.io/gorm"
)

type User struct {
	ID                    string                  `gorm:"column:id;primaryKey" json:"id"`
	FirstName             string                  `gorm:"column:firstName;default:'';not null" json:"firstName"`
	LastName              string                  `gorm:"column:lastName;default:'';not null" json:"lastName"`
	Name                  string                  `gorm:"column:name;not null" json:"name"`
	Email                 string                  `gorm:"column:email;uniqueIndex;not null" json:"email"`
	EmailVerified         bool                    `gorm:"column:emailVerified;not null" json:"emailVerified"`
	Image                 *string                 `gorm:"column:image" json:"image"`
	Locale                *string                 `gorm:"column:locale;default:'fr'" json:"locale"`
	CreatedAt             DateTime                `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime                `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Role                  *string                 `gorm:"column:role;default:'user'" json:"role"`
	Banned                *bool                   `gorm:"column:banned;default:false" json:"banned"`
	BanReason             *string                 `gorm:"column:banReason" json:"banReason"`
	BanExpires            *DateTime               `gorm:"column:banExpires" json:"banExpires"`
	IsPremium             *bool                   `gorm:"column:isPremium;default:false" json:"isPremium"`
	OnboardingPreferences datatypes.JSON          `gorm:"column:onboardingPreferences" json:"onboardingPreferences"`
	Sessions              []Session               `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Accounts              []Account               `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Feedbacks             []Feedbacks             `gorm:"foreignKey:UserID;constraint:OnDelete:SET NULL"`
	WorkoutSession        []WorkoutSession        `gorm:"foreignKey:UserID"`
	FavoriteExercises     []UserFavoriteExercise  `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Subscriptions         []Subscription          `gorm:"foreignKey:UserID"`
	Licenses              []License               `gorm:"foreignKey:UserID"`
	ProgramEnrollments    []UserProgramEnrollment `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
}

func (User) TableName() string {
	return "user"
}

type UserFavoriteExercise struct {
	ID         string   `gorm:"column:id;primaryKey" json:"id"`
	UserID     string   `gorm:"column:userId;uniqueIndex:user_favorite_exercises_userId_exerciseId_key;not null" json:"userId"`
	ExerciseID string   `gorm:"column:exerciseId;uniqueIndex:user_favorite_exercises_userId_exerciseId_key;not null" json:"exerciseId"`
	UpdatedAt  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User       *User
	Exercise   *Exercise
}

func (UserFavoriteExercise) TableName() string {
	return "user_favorite_exercises"
}

func (m *UserFavoriteExercise) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*UserFavoriteExercise) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Session struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	ExpiresAt      DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	Token          string   `gorm:"column:token;uniqueIndex;not null" json:"token"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	IPAddress      *string  `gorm:"column:ipAddress" json:"ipAddress"`
	UserAgent      *string  `gorm:"column:userAgent" json:"userAgent"`
	UserID         string   `gorm:"column:userId;not null" json:"userId"`
	ImpersonatedBy *string  `gorm:"column:impersonatedBy" json:"impersonatedBy"`
	User           *User
}

func (Session) TableName() string {
	return "session"
}

type Account struct {
	ID                    string    `gorm:"column:id;primaryKey" json:"id"`
	AccountID             string    `gorm:"column:accountId;not null" json:"accountId"`
	ProviderID            string    `gorm:"column:providerId;not null" json:"providerId"`
	UserID                string    `gorm:"column:userId;not null" json:"userId"`
	AccessToken           *string   `gorm:"column:accessToken" json:"accessToken"`
	RefreshToken          *string   `gorm:"column:refreshToken" json:"refreshToken"`
	IDToken               *string   `gorm:"column:idToken" json:"idToken"`
	AccessTokenExpiresAt  *DateTime `gorm:"column:accessTokenExpiresAt" json:"accessTokenExpiresAt"`
	RefreshTokenExpiresAt *DateTime `gorm:"column:refreshTokenExpiresAt" json:"refreshTokenExpiresAt"`
	Scope                 *string   `gorm:"column:scope" json:"scope"`
	Password              *string   `gorm:"column:password" json:"password"`
	CreatedAt             DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User                  *User
}

func (Account) TableName() string {
	return "account"
}

type Verification struct {
	ID         string    `gorm:"column:id;primaryKey" json:"id"`
	Identifier string    `gorm:"column:identifier;not null" json:"identifier"`
	Value      string    `gorm:"column:value;not null" json:"value"`
	ExpiresAt  DateTime  `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt  *DateTime `gorm:"column:createdAt;autoCreateTime:false" json:"createdAt"`
	UpdatedAt  *DateTime `gorm:"column:updatedAt;autoUpdateTime:false" json:"updatedAt"`
}

func (Verification) TableName() string {
	return "verification"
}

type Feedbacks struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	Review    int      `gorm:"column:review;not null" json:"review"`
	Message   string   `gorm:"column:message;not null" json:"message"`
	Email     *string  `gorm:"column:email" json:"email"`
	UserID    *string  `gorm:"column:userId" json:"userId"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User      *User
}

func (Feedbacks) TableName() string {
	return "feedbacks"
}

func (m *Feedbacks) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Feedbacks) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Exercise struct {
	ID                     string                   `gorm:"column:id;primaryKey" json:"id"`
	Name                   string                   `gorm:"column:name;not null" json:"name"`
	NameEn                 *string                  `gorm:"column:nameEn" json:"nameEn"`
	Description            *string                  `gorm:"column:description;type:text" json:"description"`
	DescriptionEn          *string                  `gorm:"column:descriptionEn;type:text" json:"descriptionEn"`
	FullVideoURL           *string                  `gorm:"column:fullVideoUrl;type:text" json:"fullVideoUrl"`
	FullVideoImageURL      *string                  `gorm:"column:fullVideoImageUrl;type:text" json:"fullVideoImageUrl"`
	Introduction           *string                  `gorm:"column:introduction;type:text" json:"introduction"`
	IntroductionEn         *string                  `gorm:"column:introductionEn;type:text" json:"introductionEn"`
	Slug                   *string                  `gorm:"column:slug;uniqueIndex" json:"slug"`
	SlugEn                 *string                  `gorm:"column:slugEn;uniqueIndex" json:"slugEn"`
	CreatedAt              DateTime                 `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime                 `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Attributes             []ExerciseAttribute      `gorm:"foreignKey:ExerciseID;constraint:OnDelete:CASCADE"`
	WorkoutSessionExercise []WorkoutSessionExercise `gorm:"foreignKey:ExerciseID"`
	ProgramSessionExercise []ProgramSessionExercise `gorm:"foreignKey:ExerciseID"`
	FavoritesByUsers       []UserFavoriteExercise   `gorm:"foreignKey:ExerciseID;constraint:OnDelete:CASCADE"`
}

func (Exercise) TableName() string {
	return "exercises"
}

func (m *Exercise) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Exercise) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ExerciseAttributeName struct {
	ID         string                   `gorm:"column:id;primaryKey" json:"id"`
	Name       string                   `gorm:"column:name;uniqueIndex;not null" json:"name"`
	CreatedAt  DateTime                 `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime                 `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Values     []ExerciseAttributeValue `gorm:"foreignKey:AttributeNameID"`
	Attributes []ExerciseAttribute      `gorm:"foreignKey:AttributeNameID"`
}

func (ExerciseAttributeName) TableName() string {
	return "exercise_attribute_names"
}

func (m *ExerciseAttributeName) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ExerciseAttributeName) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ExerciseAttributeValue struct {
	ID              string                 `gorm:"column:id;primaryKey" json:"id"`
	AttributeNameID string                 `gorm:"column:attributeNameId;uniqueIndex:exercise_attribute_values_attributeNameId_value_key;not null" json:"attributeNameId"`
	Value           string                 `gorm:"column:value;uniqueIndex:exercise_attribute_values_attributeNameId_value_key;not null" json:"value"`
	CreatedAt       DateTime               `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime               `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	AttributeName   *ExerciseAttributeName `gorm:"foreignKey:AttributeNameID"`
	Attributes      []ExerciseAttribute    `gorm:"foreignKey:AttributeValueID"`
}

func (ExerciseAttributeValue) TableName() string {
	return "exercise_attribute_values"
}

func (m *ExerciseAttributeValue) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ExerciseAttributeValue) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ExerciseAttribute struct {
	ID               string   `gorm:"column:id;primaryKey" json:"id"`
	ExerciseID       string   `gorm:"column:exerciseId;uniqueIndex:exercise_attributes_exerciseId_attributeNameId_attributeValueId_key;not null" json:"exerciseId"`
	AttributeNameID  string   `gorm:"column:attributeNameId;uniqueIndex:exercise_attributes_exerciseId_attributeNameId_attributeValueId_key;not null" json:"attributeNameId"`
	AttributeValueID string   `gorm:"column:attributeValueId;uniqueIndex:exercise_attributes_exerciseId_attributeNameId_attributeValueId_key;not null" json:"attributeValueId"`
	CreatedAt        DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Exercise         *Exercise
	AttributeName    *ExerciseAttributeName  `gorm:"foreignKey:AttributeNameID"`
	AttributeValue   *ExerciseAttributeValue `gorm:"foreignKey:AttributeValueID"`
}

func (ExerciseAttribute) TableName() string {
	return "exercise_attributes"
}

func (m *ExerciseAttribute) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ExerciseAttribute) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WorkoutSession struct {
	ID                  string    `gorm:"column:id;primaryKey" json:"id"`
	UserID              string    `gorm:"column:userId;not null" json:"userId"`
	StartedAt           DateTime  `gorm:"column:startedAt;not null" json:"startedAt"`
	EndedAt             *DateTime `gorm:"column:endedAt" json:"endedAt"`
	Duration            *int      `gorm:"column:duration" json:"duration"`
	Muscles             []string  `gorm:"column:muscles;serializer:json;not null" json:"muscles"`
	Rating              *int      `gorm:"column:rating" json:"rating"`
	RatingComment       *string   `gorm:"column:ratingComment;type:text" json:"ratingComment"`
	User                *User
	Exercises           []WorkoutSessionExercise `gorm:"foreignKey:WorkoutSessionID;constraint:OnDelete:CASCADE"`
	UserSessionProgress *UserSessionProgress     `gorm:"foreignKey:WorkoutSessionID"`
}

func (WorkoutSession) TableName() string {
	return "workout_sessions"
}

func (m *WorkoutSession) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type WorkoutSessionExercise struct {
	ID               string `gorm:"column:id;primaryKey" json:"id"`
	WorkoutSessionID string `gorm:"column:workoutSessionId;not null" json:"workoutSessionId"`
	ExerciseID       string `gorm:"column:exerciseId;not null" json:"exerciseId"`
	Order            int    `gorm:"column:order;not null" json:"order"`
	WorkoutSession   *WorkoutSession
	Exercise         *Exercise
	Sets             []WorkoutSet `gorm:"foreignKey:WorkoutSessionExerciseID;constraint:OnDelete:CASCADE"`
}

func (WorkoutSessionExercise) TableName() string {
	return "workout_session_exercises"
}

func (m *WorkoutSessionExercise) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type WorkoutSet struct {
	ID                       string   `gorm:"column:id;primaryKey" json:"id"`
	WorkoutSessionExerciseID string   `gorm:"column:workoutSessionExerciseId;not null" json:"workoutSessionExerciseId"`
	SetIndex                 int      `gorm:"column:setIndex;not null" json:"setIndex"`
	Type                     string   `gorm:"column:type;not null" json:"type"`
	Types                    []string `gorm:"column:types;serializer:json;not null" json:"types"`
	ValuesInt                []int    `gorm:"column:valuesInt;serializer:json;not null" json:"valuesInt"`
	ValuesSec                []int    `gorm:"column:valuesSec;serializer:json;not null" json:"valuesSec"`
	Units                    []string `gorm:"column:units;serializer:json;not null" json:"units"`
	Completed                bool     `gorm:"column:completed;default:false;not null" json:"completed"`
	WorkoutSessionExercise   *WorkoutSessionExercise
}

func (WorkoutSet) TableName() string {
	return "workout_sets"
}

func (m *WorkoutSet) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type SubscriptionPlan struct {
	ID               string                `gorm:"column:id;primaryKey" json:"id"`
	PriceMonthly     *float64              `gorm:"column:priceMonthly;type:decimal(10,2)" json:"priceMonthly"`
	PriceYearly      *float64              `gorm:"column:priceYearly;type:decimal(10,2)" json:"priceYearly"`
	Currency         *string               `gorm:"column:currency;default:'EUR';not null" json:"currency"`
	Interval         *string               `gorm:"column:interval;default:'month';not null" json:"interval"`
	IsActive         *bool                 `gorm:"column:isActive;default:true;not null" json:"isActive"`
	AvailableRegions []string              `gorm:"column:availableRegions;serializer:json;not null" json:"availableRegions"`
	CreatedAt        DateTime              `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime              `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Subscriptions    []Subscription        `gorm:"foreignKey:PlanID"`
	ProviderMappings []PlanProviderMapping `gorm:"foreignKey:PlanID;constraint:OnDelete:CASCADE"`
}

func (SubscriptionPlan) TableName() string {
	return "subscription_plans"
}

func (m *SubscriptionPlan) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SubscriptionPlan) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PlanProviderMapping struct {
	ID         string            `gorm:"column:id;primaryKey" json:"id"`
	PlanID     string            `gorm:"column:planId;uniqueIndex:plan_provider_mappings_planId_provider_region_key;not null" json:"planId"`
	Provider   string            `gorm:"column:provider;uniqueIndex:plan_provider_mappings_planId_provider_region_key;index:plan_provider_mappings_provider_externalId_idx;not null" json:"provider"`
	ExternalID string            `gorm:"column:externalId;index:plan_provider_mappings_provider_externalId_idx;not null" json:"externalId"`
	Region     *string           `gorm:"column:region;uniqueIndex:plan_provider_mappings_planId_provider_region_key" json:"region"`
	Metadata   datatypes.JSON    `gorm:"column:metadata" json:"metadata"`
	IsActive   *bool             `gorm:"column:isActive;default:true;not null" json:"isActive"`
	CreatedAt  DateTime          `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime          `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Plan       *SubscriptionPlan `gorm:"foreignKey:PlanID"`
}

func (PlanProviderMapping) TableName() string {
	return "plan_provider_mappings"
}

func (m *PlanProviderMapping) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*PlanProviderMapping) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Subscription struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	UserID           string    `gorm:"column:userId;uniqueIndex:subscriptions_userId_platform_key;not null" json:"userId"`
	PlanID           string    `gorm:"column:planId;not null" json:"planId"`
	RevenueCatUserID *string   `gorm:"column:revenueCatUserId" json:"revenueCatUserId"`
	Status           string    `gorm:"column:status;not null" json:"status"`
	StartedAt        DateTime  `gorm:"column:startedAt;not null" json:"startedAt"`
	CurrentPeriodEnd *DateTime `gorm:"column:currentPeriodEnd" json:"currentPeriodEnd"`
	CancelledAt      *DateTime `gorm:"column:cancelledAt" json:"cancelledAt"`
	Platform         *string   `gorm:"column:platform;uniqueIndex:subscriptions_userId_platform_key" json:"platform"`
	CreatedAt        DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User             *User
	Plan             *SubscriptionPlan `gorm:"foreignKey:PlanID"`
}

func (Subscription) TableName() string {
	return "subscriptions"
}

func (m *Subscription) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Subscription) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type License struct {
	ID            string         `gorm:"column:id;primaryKey" json:"id"`
	Key           string         `gorm:"column:key;uniqueIndex;not null" json:"key"`
	UserID        *string        `gorm:"column:userId" json:"userId"`
	ValidFrom     DateTime       `gorm:"column:validFrom;not null" json:"validFrom"`
	ValidUntil    *DateTime      `gorm:"column:validUntil" json:"validUntil"`
	MaxUsers      *int           `gorm:"column:maxUsers;default:1" json:"maxUsers"`
	Features      datatypes.JSON `gorm:"column:features" json:"features"`
	ActivatedAt   *DateTime      `gorm:"column:activatedAt" json:"activatedAt"`
	LastCheckedAt *DateTime      `gorm:"column:lastCheckedAt" json:"lastCheckedAt"`
	CreatedAt     DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	User          *User
}

func (License) TableName() string {
	return "licenses"
}

func (m *License) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.ValidFrom.IsZero() {
		m.ValidFrom = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type RevenueCatWebhookEvent struct {
	ID                    string         `gorm:"column:id;primaryKey" json:"id"`
	EventType             string         `gorm:"column:eventType;index:revenuecat_webhook_events_eventType_idx;not null" json:"eventType"`
	EventTimestamp        DateTime       `gorm:"column:eventTimestamp;index:revenuecat_webhook_events_eventTimestamp_idx;not null" json:"eventTimestamp"`
	AppUserID             string         `gorm:"column:appUserId;index:revenuecat_webhook_events_appUserId_idx;not null" json:"appUserId"`
	Environment           string         `gorm:"column:environment;not null" json:"environment"`
	ProductID             *string        `gorm:"column:productId" json:"productId"`
	TransactionID         *string        `gorm:"column:transactionId" json:"transactionId"`
	OriginalTransactionID *string        `gorm:"column:originalTransactionId" json:"originalTransactionId"`
	EntitlementIds        *string        `gorm:"column:entitlementIds" json:"entitlementIds"`
	Processed             bool           `gorm:"column:processed;index:revenuecat_webhook_events_processed_idx;default:false;not null" json:"processed"`
	ProcessingError       *string        `gorm:"column:processingError;type:text" json:"processingError"`
	RetryCount            int            `gorm:"column:retryCount;default:0;not null" json:"retryCount"`
	RawEventData          datatypes.JSON `gorm:"column:rawEventData;not null" json:"rawEventData"`
	CreatedAt             DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (RevenueCatWebhookEvent) TableName() string {
	return "revenuecat_webhook_events"
}

func (m *RevenueCatWebhookEvent) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*RevenueCatWebhookEvent) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Program struct {
	ID                 string                  `gorm:"column:id;primaryKey" json:"id"`
	Slug               string                  `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	SlugEn             string                  `gorm:"column:slugEn;uniqueIndex;not null" json:"slugEn"`
	SlugEs             string                  `gorm:"column:slugEs;uniqueIndex;not null" json:"slugEs"`
	SlugPt             string                  `gorm:"column:slugPt;uniqueIndex;not null" json:"slugPt"`
	SlugRu             string                  `gorm:"column:slugRu;uniqueIndex;not null" json:"slugRu"`
	SlugZhCn           string                  `gorm:"column:slugZhCn;uniqueIndex;not null" json:"slugZhCn"`
	Title              string                  `gorm:"column:title;not null" json:"title"`
	TitleEn            string                  `gorm:"column:titleEn;not null" json:"titleEn"`
	TitleEs            string                  `gorm:"column:titleEs;not null" json:"titleEs"`
	TitlePt            string                  `gorm:"column:titlePt;not null" json:"titlePt"`
	TitleRu            string                  `gorm:"column:titleRu;not null" json:"titleRu"`
	TitleZhCn          string                  `gorm:"column:titleZhCn;not null" json:"titleZhCn"`
	Description        string                  `gorm:"column:description;type:text;not null" json:"description"`
	DescriptionEn      string                  `gorm:"column:descriptionEn;type:text;not null" json:"descriptionEn"`
	DescriptionEs      string                  `gorm:"column:descriptionEs;type:text;not null" json:"descriptionEs"`
	DescriptionPt      string                  `gorm:"column:descriptionPt;type:text;not null" json:"descriptionPt"`
	DescriptionRu      string                  `gorm:"column:descriptionRu;type:text;not null" json:"descriptionRu"`
	DescriptionZhCn    string                  `gorm:"column:descriptionZhCn;type:text;not null" json:"descriptionZhCn"`
	Category           string                  `gorm:"column:category;not null" json:"category"`
	Image              string                  `gorm:"column:image;not null" json:"image"`
	Level              string                  `gorm:"column:level;not null" json:"level"`
	Type               string                  `gorm:"column:type;not null" json:"type"`
	DurationWeeks      *int                    `gorm:"column:durationWeeks;default:4;not null" json:"durationWeeks"`
	SessionsPerWeek    *int                    `gorm:"column:sessionsPerWeek;default:3;not null" json:"sessionsPerWeek"`
	SessionDurationMin *int                    `gorm:"column:sessionDurationMin;default:30;not null" json:"sessionDurationMin"`
	Equipment          []string                `gorm:"column:equipment;serializer:json;not null" json:"equipment"`
	IsPremium          *bool                   `gorm:"column:isPremium;default:true;not null" json:"isPremium"`
	IsActive           *bool                   `gorm:"column:isActive;default:true;not null" json:"isActive"`
	Visibility         string                  `gorm:"column:visibility;default:'DRAFT';not null" json:"visibility"`
	ParticipantCount   int                     `gorm:"column:participantCount;default:0;not null" json:"participantCount"`
	CreatedAt          DateTime                `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime                `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Weeks              []ProgramWeek           `gorm:"foreignKey:ProgramID;constraint:OnDelete:CASCADE"`
	Enrollments        []UserProgramEnrollment `gorm:"foreignKey:ProgramID"`
	Coaches            []ProgramCoach          `gorm:"foreignKey:ProgramID;constraint:OnDelete:CASCADE"`
}

func (Program) TableName() string {
	return "programs"
}

func (m *Program) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Program) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ProgramCoach struct {
	ID        string `gorm:"column:id;primaryKey" json:"id"`
	ProgramID string `gorm:"column:programId;not null" json:"programId"`
	Name      string `gorm:"column:name;not null" json:"name"`
	Image     string `gorm:"column:image;not null" json:"image"`
	Order     int    `gorm:"column:order;default:0;not null" json:"order"`
	Program   *Program
}

func (ProgramCoach) TableName() string {
	return "program_coaches"
}

func (m *ProgramCoach) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ProgramWeek struct {
	ID              string `gorm:"column:id;primaryKey" json:"id"`
	ProgramID       string `gorm:"column:programId;uniqueIndex:program_weeks_programId_weekNumber_key;not null" json:"programId"`
	WeekNumber      int    `gorm:"column:weekNumber;uniqueIndex:program_weeks_programId_weekNumber_key;not null" json:"weekNumber"`
	Title           string `gorm:"column:title;not null" json:"title"`
	TitleEn         string `gorm:"column:titleEn;not null" json:"titleEn"`
	TitleEs         string `gorm:"column:titleEs;not null" json:"titleEs"`
	TitlePt         string `gorm:"column:titlePt;not null" json:"titlePt"`
	TitleRu         string `gorm:"column:titleRu;not null" json:"titleRu"`
	TitleZhCn       string `gorm:"column:titleZhCn;not null" json:"titleZhCn"`
	Description     string `gorm:"column:description;type:text;not null" json:"description"`
	DescriptionEn   string `gorm:"column:descriptionEn;type:text;not null" json:"descriptionEn"`
	DescriptionEs   string `gorm:"column:descriptionEs;type:text;not null" json:"descriptionEs"`
	DescriptionPt   string `gorm:"column:descriptionPt;type:text;not null" json:"descriptionPt"`
	DescriptionRu   string `gorm:"column:descriptionRu;type:text;not null" json:"descriptionRu"`
	DescriptionZhCn string `gorm:"column:descriptionZhCn;type:text;not null" json:"descriptionZhCn"`
	Program         *Program
	Sessions        []ProgramSession `gorm:"foreignKey:WeekID;constraint:OnDelete:CASCADE"`
}

func (ProgramWeek) TableName() string {
	return "program_weeks"
}

func (m *ProgramWeek) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ProgramSession struct {
	ID               string                   `gorm:"column:id;primaryKey" json:"id"`
	WeekID           string                   `gorm:"column:weekId;uniqueIndex:program_sessions_weekId_sessionNumber_key;uniqueIndex:program_sessions_weekId_slug_key;uniqueIndex:program_sessions_weekId_slugEn_key;uniqueIndex:program_sessions_weekId_slugEs_key;uniqueIndex:program_sessions_weekId_slugPt_key;uniqueIndex:program_sessions_weekId_slugRu_key;uniqueIndex:program_sessions_weekId_slugZhCn_key;not null" json:"weekId"`
	SessionNumber    int                      `gorm:"column:sessionNumber;uniqueIndex:program_sessions_weekId_sessionNumber_key;not null" json:"sessionNumber"`
	Title            string                   `gorm:"column:title;not null" json:"title"`
	TitleEn          string                   `gorm:"column:titleEn;not null" json:"titleEn"`
	TitleEs          string                   `gorm:"column:titleEs;not null" json:"titleEs"`
	TitlePt          string                   `gorm:"column:titlePt;not null" json:"titlePt"`
	TitleRu          string                   `gorm:"column:titleRu;not null" json:"titleRu"`
	TitleZhCn        string                   `gorm:"column:titleZhCn;not null" json:"titleZhCn"`
	Slug             string                   `gorm:"column:slug;uniqueIndex:program_sessions_weekId_slug_key;not null" json:"slug"`
	SlugEn           string                   `gorm:"column:slugEn;uniqueIndex:program_sessions_weekId_slugEn_key;not null" json:"slugEn"`
	SlugEs           string                   `gorm:"column:slugEs;uniqueIndex:program_sessions_weekId_slugEs_key;not null" json:"slugEs"`
	SlugPt           string                   `gorm:"column:slugPt;uniqueIndex:program_sessions_weekId_slugPt_key;not null" json:"slugPt"`
	SlugRu           string                   `gorm:"column:slugRu;uniqueIndex:program_sessions_weekId_slugRu_key;not null" json:"slugRu"`
	SlugZhCn         string                   `gorm:"column:slugZhCn;uniqueIndex:program_sessions_weekId_slugZhCn_key;not null" json:"slugZhCn"`
	Description      string                   `gorm:"column:description;type:text;not null" json:"description"`
	DescriptionEn    string                   `gorm:"column:descriptionEn;type:text;not null" json:"descriptionEn"`
	DescriptionEs    string                   `gorm:"column:descriptionEs;type:text;not null" json:"descriptionEs"`
	DescriptionPt    string                   `gorm:"column:descriptionPt;type:text;not null" json:"descriptionPt"`
	DescriptionRu    string                   `gorm:"column:descriptionRu;type:text;not null" json:"descriptionRu"`
	DescriptionZhCn  string                   `gorm:"column:descriptionZhCn;type:text;not null" json:"descriptionZhCn"`
	Equipment        []string                 `gorm:"column:equipment;serializer:json;not null" json:"equipment"`
	EstimatedMinutes int                      `gorm:"column:estimatedMinutes;not null" json:"estimatedMinutes"`
	IsPremium        *bool                    `gorm:"column:isPremium;default:true;not null" json:"isPremium"`
	Week             *ProgramWeek             `gorm:"foreignKey:WeekID"`
	Exercises        []ProgramSessionExercise `gorm:"foreignKey:SessionID;constraint:OnDelete:CASCADE"`
	UserProgress     []UserSessionProgress    `gorm:"foreignKey:SessionID"`
}

func (ProgramSession) TableName() string {
	return "program_sessions"
}

func (m *ProgramSession) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ProgramSessionExercise struct {
	ID               string          `gorm:"column:id;primaryKey" json:"id"`
	SessionID        string          `gorm:"column:sessionId;uniqueIndex:program_session_exercises_sessionId_order_key;not null" json:"sessionId"`
	ExerciseID       string          `gorm:"column:exerciseId;not null" json:"exerciseId"`
	Order            int             `gorm:"column:order;uniqueIndex:program_session_exercises_sessionId_order_key;not null" json:"order"`
	Instructions     string          `gorm:"column:instructions;type:text;not null" json:"instructions"`
	InstructionsEn   string          `gorm:"column:instructionsEn;type:text;not null" json:"instructionsEn"`
	InstructionsEs   string          `gorm:"column:instructionsEs;type:text;not null" json:"instructionsEs"`
	InstructionsPt   string          `gorm:"column:instructionsPt;type:text;not null" json:"instructionsPt"`
	InstructionsRu   string          `gorm:"column:instructionsRu;type:text;not null" json:"instructionsRu"`
	InstructionsZhCn string          `gorm:"column:instructionsZhCn;type:text;not null" json:"instructionsZhCn"`
	Session          *ProgramSession `gorm:"foreignKey:SessionID"`
	Exercise         *Exercise
	SuggestedSets    []ProgramSuggestedSet `gorm:"foreignKey:ProgramSessionExerciseID;constraint:OnDelete:CASCADE"`
}

func (ProgramSessionExercise) TableName() string {
	return "program_session_exercises"
}

func (m *ProgramSessionExercise) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ProgramSuggestedSet struct {
	ID                       string   `gorm:"column:id;primaryKey" json:"id"`
	ProgramSessionExerciseID string   `gorm:"column:programSessionExerciseId;uniqueIndex:program_suggested_sets_programSessionExerciseId_setIndex_key;not null" json:"programSessionExerciseId"`
	SetIndex                 int      `gorm:"column:setIndex;uniqueIndex:program_suggested_sets_programSessionExerciseId_setIndex_key;not null" json:"setIndex"`
	Types                    []string `gorm:"column:types;serializer:json;not null" json:"types"`
	ValuesInt                []int    `gorm:"column:valuesInt;serializer:json;not null" json:"valuesInt"`
	ValuesSec                []int    `gorm:"column:valuesSec;serializer:json;not null" json:"valuesSec"`
	Units                    []string `gorm:"column:units;serializer:json;not null" json:"units"`
	ProgramSessionExercise   *ProgramSessionExercise
}

func (ProgramSuggestedSet) TableName() string {
	return "program_suggested_sets"
}

func (m *ProgramSuggestedSet) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type UserProgramEnrollment struct {
	ID                string    `gorm:"column:id;primaryKey" json:"id"`
	UserID            string    `gorm:"column:userId;uniqueIndex:user_program_enrollments_userId_programId_key;not null" json:"userId"`
	ProgramID         string    `gorm:"column:programId;uniqueIndex:user_program_enrollments_userId_programId_key;not null" json:"programId"`
	EnrolledAt        DateTime  `gorm:"column:enrolledAt;not null" json:"enrolledAt"`
	CurrentWeek       *int      `gorm:"column:currentWeek;default:1;not null" json:"currentWeek"`
	CurrentSession    *int      `gorm:"column:currentSession;default:1;not null" json:"currentSession"`
	CompletedSessions int       `gorm:"column:completedSessions;default:0;not null" json:"completedSessions"`
	IsActive          *bool     `gorm:"column:isActive;default:true;not null" json:"isActive"`
	CompletedAt       *DateTime `gorm:"column:completedAt" json:"completedAt"`
	User              *User
	Program           *Program
	SessionProgress   []UserSessionProgress `gorm:"foreignKey:EnrollmentID;constraint:OnDelete:CASCADE"`
}

func (UserProgramEnrollment) TableName() string {
	return "user_program_enrollments"
}

func (m *UserProgramEnrollment) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.EnrolledAt.IsZero() {
		m.EnrolledAt = DateTime{Time: now}
	}
	return nil
}

type UserSessionProgress struct {
	ID               string                 `gorm:"column:id;primaryKey" json:"id"`
	EnrollmentID     string                 `gorm:"column:enrollmentId;uniqueIndex:user_session_progress_enrollmentId_sessionId_key;not null" json:"enrollmentId"`
	SessionID        string                 `gorm:"column:sessionId;uniqueIndex:user_session_progress_enrollmentId_sessionId_key;not null" json:"sessionId"`
	StartedAt        DateTime               `gorm:"column:startedAt;not null" json:"startedAt"`
	CompletedAt      *DateTime              `gorm:"column:completedAt" json:"completedAt"`
	WorkoutSessionID *string                `gorm:"column:workoutSessionId;uniqueIndex" json:"workoutSessionId"`
	Enrollment       *UserProgramEnrollment `gorm:"foreignKey:EnrollmentID"`
	Session          *ProgramSession        `gorm:"foreignKey:SessionID"`
	WorkoutSession   *WorkoutSession
}

func (UserSessionProgress) TableName() string {
	return "user_session_progress"
}

func (m *UserSessionProgress) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.StartedAt.IsZero() {
		m.StartedAt = DateTime{Time: now}
	}
	return nil
}

type DateTime struct{ time.Time }

func (DateTime) GormDataType() string {
	return "time"
}

func (dateTime DateTime) Value() (driver.Value, error) {
	return dateTime.UTC().Truncate(time.Millisecond), nil
}

func (dateTime *DateTime) Scan(src any) error {
	read, err := readPrismaTime(src)
	dateTime.Time = read
	return err
}

var prismaTimeLayouts = []string{
	"2006-01-02T15:04:05.999999999Z07:00",
	"2006-01-02 15:04:05.999999999Z07:00",
	"2006-01-02 15:04:05.999999999Z07",
	"2006-01-02T15:04:05.999999999",
	"2006-01-02 15:04:05.999999999",
	"2006-01-02",
}

func readPrismaTime(src any) (time.Time, error) {
	switch value := src.(type) {
	case nil:
		return time.Time{}, nil
	case time.Time:
		return value.UTC().Truncate(time.Millisecond), nil
	case int64:
		return time.UnixMilli(value).UTC(), nil
	case []byte:
		return readPrismaTime(string(value))
	case string:
		if millis, err := strconv.ParseInt(value, 10, 64); err == nil {
			return time.UnixMilli(millis).UTC(), nil
		}
		for _, layout := range prismaTimeLayouts {
			if read, err := time.Parse(layout, value); err == nil {
				return read.UTC().Truncate(time.Millisecond), nil
			}
		}
		return time.Time{}, fmt.Errorf("a DateTime column held %q", value)
	}
	return time.Time{}, fmt.Errorf("a DateTime column held %T", src)
}
