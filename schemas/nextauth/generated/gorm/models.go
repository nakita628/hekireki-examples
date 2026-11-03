package model

import (
	"database/sql/driver"
	"fmt"
	"strconv"
	"time"

	"github.com/lucsky/cuid"
	"gorm.io/gorm"
)

type User struct {
	ID            string          `gorm:"column:id;primaryKey" json:"id"`
	Name          *string         `gorm:"column:name" json:"name"`
	Email         string          `gorm:"column:email;uniqueIndex;not null" json:"email"`
	EmailVerified *DateTime       `gorm:"column:emailVerified" json:"emailVerified"`
	Image         *string         `gorm:"column:image" json:"image"`
	Accounts      []Account       `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Sessions      []Session       `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Authenticator []Authenticator `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
}

func (User) TableName() string {
	return "User"
}

func (m *User) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type Account struct {
	UserID            string  `gorm:"column:userId;not null" json:"userId"`
	Type              string  `gorm:"column:type;not null" json:"type"`
	Provider          string  `gorm:"column:provider;primaryKey" json:"provider"`
	ProviderAccountID string  `gorm:"column:providerAccountId;primaryKey" json:"providerAccountId"`
	RefreshToken      *string `gorm:"column:refresh_token" json:"refresh_token"`
	AccessToken       *string `gorm:"column:access_token" json:"access_token"`
	ExpiresAt         *int    `gorm:"column:expires_at" json:"expires_at"`
	TokenType         *string `gorm:"column:token_type" json:"token_type"`
	Scope             *string `gorm:"column:scope" json:"scope"`
	IDToken           *string `gorm:"column:id_token" json:"id_token"`
	SessionState      *string `gorm:"column:session_state" json:"session_state"`
	User              User
}

func (Account) TableName() string {
	return "Account"
}

type VerificationToken struct {
	Identifier string   `gorm:"column:identifier;primaryKey" json:"identifier"`
	Token      string   `gorm:"column:token;primaryKey;uniqueIndex" json:"token"`
	Expires    DateTime `gorm:"column:expires;not null" json:"expires"`
}

func (VerificationToken) TableName() string {
	return "VerificationToken"
}

type Authenticator struct {
	CredentialID         string  `gorm:"column:credentialID;primaryKey;uniqueIndex" json:"credentialID"`
	UserID               string  `gorm:"column:userId;primaryKey" json:"userId"`
	ProviderAccountID    string  `gorm:"column:providerAccountId;not null" json:"providerAccountId"`
	CredentialPublicKey  string  `gorm:"column:credentialPublicKey;not null" json:"credentialPublicKey"`
	Counter              int     `gorm:"column:counter;not null" json:"counter"`
	CredentialDeviceType string  `gorm:"column:credentialDeviceType;not null" json:"credentialDeviceType"`
	CredentialBackedUp   bool    `gorm:"column:credentialBackedUp;not null" json:"credentialBackedUp"`
	Transports           *string `gorm:"column:transports" json:"transports"`
	User                 User
}

func (Authenticator) TableName() string {
	return "Authenticator"
}

type DateTime struct{ time.Time }

func (DateTime) GormDataType() string {
	return "time"
}

func (dateTime DateTime) Value() (driver.Value, error) {
	return dateTime.UTC().Truncate(time.Millisecond).Format("2006-01-02T15:04:05.000-07:00"), nil
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
